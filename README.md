# PrimeNumber —— 质数判断算法竞技场

本项目旨在收集、比较和筛选高效的**质数判断算法**。  
所有参赛算法通过 GitHub Pull Request 提交，由 CI 自动验证**正确性**与**性能**，性能进入前 10 的算法才能合并至主分支。

---

## 项目结构

```
PrimeNumber/
├── README.md                        # 项目说明（本文件）
├── shared/
│   └── PrimeAlgorithm.Contracts/    # 所有算法必须实现的统一接口
│       └── IPrimeChecker.cs
├── algorithms/                      # 各算法独立目录
│   ├── sieve-of-eratosthenes/       # 埃拉托色尼筛法
│   │   ├── README.md
│   │   ├── SieveOfEratosthenes/     # 算法实现（.NET 10 类库）
│   │   └── SieveOfEratosthenes.Tests/  # 单元测试
│   └── trial-division/              # 试除法
│       ├── README.md
│       ├── TrialDivision/
│       └── TrialDivision.Tests/
├── benchmarks/
│   └── AlgorithmBenchmarks/         # 统一基准测试项目（BenchmarkDotNet）
├── docs/
│   └── performance-rankings.md      # 自动维护的性能排行榜
└── .github/
    ├── workflows/
    │   ├── validate-pr.yml          # PR 验证：正确性 + 性能
    │   └── tag-release.yml          # 定期打标签 + 清理
    └── scripts/
        ├── check_ranking.py         # 检查排名脚本
        └── update_rankings.py       # 更新排行榜脚本
```

---

## 参与方式：如何提交新算法

1. **Fork** 本仓库并创建新分支。
2. 在 `algorithms/` 下新建以算法命名的目录，例如 `algorithms/miller-rabin/`。
3. 目录结构须包含：
   - **算法实现**（.NET 10 类库项目）：实现 `IPrimeChecker` 接口。
   - **单元测试**（.NET 10 xUnit 测试项目）：覆盖边界用例及已知质数/合数。
   - **README.md**：算法简介（中文），包括复杂度说明。
4. 在 `benchmarks/AlgorithmBenchmarks/` 的 `Program.cs` 中注册新算法。
5. 更新本 `README.md` 的「当前算法列表」章节。
6. 提交 PR，CI 将自动：
   - 运行所有算法的单元测试（正确性验证）。
   - 运行基准测试并检查新算法是否进入前 10 名。
   - 在 PR 页面发布性能测试结果评论。
7. 性能排名**未能进入前 10** 的 PR，CI 将失败并阻止合并。

### 算法实现要求

```csharp
using PrimeAlgorithm.Contracts;

namespace MyAlgorithm;

public class MyPrimeChecker : IPrimeChecker
{
    public string AlgorithmName => "我的算法名称（中文）";
    public bool IsPrime(long n) { /* ... */ }
    public IEnumerable<long> GetPrimesUpTo(long max) { /* ... */ }
}
```

---

## 当前算法列表

### 1. 埃拉托色尼筛法 (Sieve of Eratosthenes)

**目录：** `algorithms/sieve-of-eratosthenes/`

古希腊数学家埃拉托色尼发明的经典筛法。从 2 开始，依次将每个质数的所有倍数标记为合数，遍历结束后未被标记的数即为质数。

- 时间复杂度：O(n log log n)
- 空间复杂度：O(n)
- 适用场景：一次性生成某范围内所有质数

---

### 2. 试除法 (Trial Division)

**目录：** `algorithms/trial-division/`

最基础的质数判断算法。对于数 n，仅需检验 2 到 √n 之间所有可能的除数，本实现使用 6k±1 优化跳过明显的合数候选。

- 时间复杂度（单次判断）：O(√n)
- 空间复杂度：O(1)
- 适用场景：单次质数判断，无需预处理

---

### 3. Baillie-PSW 素性测试 (Baillie-PSW Primality Test)

**目录：** `algorithms/baillie-psw/`

由 Robert Baillie、Carl Pomerance、John Selfridge 和 Samuel Wagstaff 于 1980 年提出，将 **强 Miller-Rabin 基-2 测试** 与 **强 Lucas 伪素数测试**（Selfridge 方法 A）结合。两种测试独立互补，在 2^80 范围内无已知假阳性（伪素数）。

- 时间复杂度（单次判断）：O(log² n)
- 空间复杂度：O(1)
- 适用场景：单次质数判断，尤其适用于对正确性要求极高的场景

---

## 性能排行榜

详见 [docs/performance-rankings.md](docs/performance-rankings.md)。

排行榜每周自动更新，仅**前 10 名**的算法可合并至主分支，**超出第 30 名**的算法将定期清理。

---

## 本地开发

### 环境要求

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### 运行测试

```bash
# 运行某个算法的测试
dotnet test algorithms/sieve-of-eratosthenes/SieveOfEratosthenes.Tests/SieveOfEratosthenes.Tests.csproj

# 运行所有算法测试
find algorithms -name '*.Tests.csproj' -exec dotnet test {} \;
```

### 运行基准测试

```bash
# 快速性能比较（CI 使用）
dotnet run --project benchmarks/AlgorithmBenchmarks/AlgorithmBenchmarks.csproj \
  --configuration Release -- --simple

# 完整 BenchmarkDotNet 报告
dotnet run --project benchmarks/AlgorithmBenchmarks/AlgorithmBenchmarks.csproj \
  --configuration Release
```

---

## 许可证

[LICENSE](LICENSE)
