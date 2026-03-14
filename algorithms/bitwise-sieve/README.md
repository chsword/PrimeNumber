# 位压缩筛法 (Bitwise Sieve)

## 算法简介

位压缩筛法是埃拉托色尼筛法的内存优化版本，使用位数组（`ulong[]`）代替布尔数组，并只表示奇数，将内存占用降至原来的约 1/16。

**基本思路：**
- 偶数（除 2 外）直接跳过；
- 用 `ulong` 数组的每一个位表示一个奇数是否为质数；
- 按标准埃氏筛流程筛去奇数的奇倍数。

**复杂度：**
- 时间复杂度：O(n log log n)
- 空间复杂度：O(n/16)（相较标准筛节省约 16 倍内存）

## 算法实现

- 实现类：`BitwiseSievePrimeChecker`
- 实现接口：`IPrimeChecker`

## 测试

```bash
dotnet test BitwiseSieve.Tests/BitwiseSieve.Tests.csproj
```
