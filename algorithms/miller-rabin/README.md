# 米勒-拉宾素性测试 (Miller-Rabin Primality Test)

## 算法简介

米勒-拉宾素性测试由 Gary L. Miller 和 Michael O. Rabin 分别于 1976 年和 1980 年提出，是一种基于费马小定理扩展的素性测试算法。

**基本思路：**
将 `n-1` 写成 `2^r × d` 的形式，对于一组见证数 `a`，验证强伪素数条件：`a^d ≡ 1 (mod n)` 或存在 `j` 使得 `a^(2^j × d) ≡ -1 (mod n)`。本实现使用确定性见证集合 `{2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37}`，对所有 `long` 范围内的数均给出正确结果。

**复杂度：**
- 时间复杂度（单次检测）：O(k log² n)，k 为见证数个数
- 空间复杂度：O(1)

## 算法实现

- 实现类：`MillerRabinPrimeChecker`
- 实现接口：`IPrimeChecker`

## 测试

```bash
dotnet test MillerRabin.Tests/MillerRabin.Tests.csproj
```
