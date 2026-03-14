# 分段筛法 (Segmented Sieve of Eratosthenes)

## 算法简介

分段筛法是埃拉托色尼筛法的缓存友好改进版本，通过将筛区间分成大小为 `√n` 的段来减少内存用量，提升 CPU 缓存命中率。

**基本思路：**
1. 先用普通埃氏筛求出 `[2, √n]` 内所有质数；
2. 将 `[√n+1, n]` 分成长度为 `√n` 的段，对每段用已知小质数筛去合数。

**复杂度：**
- 时间复杂度：O(n log log n)
- 空间复杂度：O(√n)

## 算法实现

- 实现类：`SegmentedSievePrimeChecker`
- 实现接口：`IPrimeChecker`

## 测试

```bash
dotnet test SegmentedSieve.Tests/SegmentedSieve.Tests.csproj
```
