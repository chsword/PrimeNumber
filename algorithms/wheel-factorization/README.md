# 轮式因式分解法 (Wheel Factorization, Wheel-30)

## 算法简介

轮式因式分解是试除法的高效改进，通过预先排除 2、3、5 的倍数，仅对模 30 余数为 `{1, 7, 11, 13, 17, 19, 23, 29}` 的数进行试除，将需要检验的候选除数减少约 73%。

**基本思路：**
以 30 为一个"轮"周期，只检验该周期内与 30 互质的 8 个余数对应的数作为候选因子。

**复杂度：**
- 时间复杂度：O(√n / ln n) 每次检测（相比朴素试除节省约 73%）
- 空间复杂度：O(1)

## 算法实现

- 实现类：`WheelFactorizationPrimeChecker`
- 实现接口：`IPrimeChecker`

## 测试

```bash
dotnet test WheelFactorization.Tests/WheelFactorization.Tests.csproj
```
