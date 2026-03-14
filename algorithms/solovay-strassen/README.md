# 索洛维-斯特拉森素性测试 (Solovay-Strassen Primality Test)

## 算法简介

索洛维-斯特拉森素性测试由 Robert M. Solovay 和 Volker Strassen 于 1977 年提出，基于欧拉准则和雅可比符号，是最早实用的随机素性测试算法之一。

**基本思路：**
对奇数 `n` 和整数 `a`，若 `n` 为质数则必有 `a^((n-1)/2) ≡ (a/n) (mod n)`，其中 `(a/n)` 是雅可比符号。本实现使用确定性见证集合 `{2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37}`，对所有 `long` 范围内的数均给出正确结果。

**复杂度：**
- 时间复杂度（单次检测）：O(k log² n)，k 为见证数个数
- 空间复杂度：O(1)

## 算法实现

- 实现类：`SolovayStrassenPrimeChecker`
- 实现接口：`IPrimeChecker`

## 测试

```bash
dotnet test SolovayStrassen.Tests/SolovayStrassen.Tests.csproj
```
