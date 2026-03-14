# 欧拉线性筛 (Euler's Linear Sieve)

## 算法简介

欧拉线性筛（也称线性筛）由欧拉的思想衍生而来，是埃拉托色尼筛法的优化版本。其核心思想是保证每个合数只被其最小质因子筛去一次。

**基本思路：**
维护一个已发现质数的列表。对每个数 `i`，如果它未被标记，则加入质数列表。对每个质数 `p`（不超过 `i` 的最小质因子），标记 `i × p` 为合数。这样每个合数恰好被处理一次。

**复杂度：**
- 时间复杂度：O(n)（真正的线性复杂度）
- 空间复杂度：O(n)

## 算法实现

- 实现类：`LinearSievePrimeChecker`
- 实现接口：`IPrimeChecker`

## 测试

```bash
dotnet test LinearSieve.Tests/LinearSieve.Tests.csproj
```
