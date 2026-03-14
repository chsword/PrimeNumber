# 孙德兰筛法 (Sieve of Sundaram)

## 算法简介

孙德兰筛法由印度数学家 S. P. Sundaram 于 1934 年提出，通过排除形如 `i + j + 2ij`（其中 `1 ≤ i ≤ j`）的整数来生成质数。

**基本思路：**
对于范围 `[1, k]`，标记所有满足 `i + j + 2ij ≤ k`（`1 ≤ i ≤ j`）的数，未被标记的数 `n` 对应质数 `2n+1`；最后再加上 `2` 本身。

**复杂度：**
- 时间复杂度：O(n log n)
- 空间复杂度：O(n)

## 算法实现

- 实现类：`SieveOfSundaramPrimeChecker`
- 实现接口：`IPrimeChecker`

## 测试

```bash
dotnet test SieveOfSundaram.Tests/SieveOfSundaram.Tests.csproj
```
