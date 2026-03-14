# 试除法 (Trial Division)

## 算法简介

试除法是最基础的质数判断算法，通过逐一尝试用小于其平方根的数整除待检测数来判断是否为质数。

**基本思路：**
对于数 n，依次检验 2 到 √n 之间所有整数是否能整除 n，若均不能整除则 n 为质数。
本实现使用 6k±1 优化，跳过所有不可能为质数的候选除数。

**复杂度：**
- 时间复杂度（单次判断）：O(√n)
- 时间复杂度（生成到 n 的所有质数）：O(n√n)
- 空间复杂度：O(1)

## 算法实现

- 实现类：`TrialDivisionPrimeChecker`
- 实现接口：`IPrimeChecker`

## 测试

```bash
dotnet test TrialDivision.Tests/TrialDivision.Tests.csproj
```
