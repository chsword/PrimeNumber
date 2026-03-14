# 阿特金筛法 (Sieve of Atkin)

## 算法简介

阿特金筛法由 A. O. L. Atkin 和 Daniel J. Bernstein 于 2003 年提出，是一种对埃拉托色尼筛法进行改进的现代筛法。

**基本思路：**
利用三种特定的二次方程（`4x²+y²`、`3x²+y²`、`3x²-y²`）的解来翻转候选数的素性标志，再去掉完全平方数的倍数，剩余被标记的数即为质数。

**复杂度：**
- 时间复杂度：O(n / log log n)
- 空间复杂度：O(n)

## 算法实现

- 实现类：`SieveOfAtkinPrimeChecker`
- 实现接口：`IPrimeChecker`

## 测试

```bash
dotnet test SieveOfAtkin.Tests/SieveOfAtkin.Tests.csproj
```
