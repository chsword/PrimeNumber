# 埃拉托色尼筛法 (Sieve of Eratosthenes)

## 算法简介

埃拉托色尼筛法是一种古老且高效的质数生成算法，由古希腊数学家埃拉托色尼发明。

**基本思路：**
从 2 开始，将每个质数的倍数标记为合数，未被标记的数即为质数。

**复杂度：**
- 时间复杂度：O(n log log n)
- 空间复杂度：O(n)

## 算法实现

- 实现类：`SieveOfEratosthenesPrimeChecker`
- 实现接口：`IPrimeChecker`

## 测试

```bash
dotnet test SieveOfEratosthenes.Tests/SieveOfEratosthenes.Tests.csproj
```
