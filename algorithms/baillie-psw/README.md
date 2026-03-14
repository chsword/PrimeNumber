# Baillie-PSW 素性测试 (Baillie-PSW Primality Test)

## 算法简介

Baillie-PSW 素性测试由 Robert Baillie、Carl Pomerance、John Selfridge 和 Samuel Wagstaff 于 1980 年提出，是目前最强的概率性素性测试之一。

该算法将两种独立的测试结合起来，相互补充：

1. **强 Miller-Rabin 基-2 测试**：利用费马小定理的扩展，将 `n-1` 写成 `2^r × d`（d 为奇数），验证 `2^d ≡ ±1 (mod n)` 或存在某个平方步使结果等于 -1。

2. **强 Lucas 伪素数测试**（Selfridge 方法 A）：在序列 5, -7, 9, -11, 13, ... 中找到第一个满足 Jacobi(D, n) = -1 的 D，令 P = 1，Q = (1 - D) / 4，将 n + 1 写成 `2^s × d`（d 为奇数），验证 U_d ≡ 0 或某个 V_{2^j·d} ≡ 0 (mod n)。

**核心优势：** Miller-Rabin 与 Lucas 测试在本质上独立——同一个合数通过两种测试的概率极低。在所有已知范围（包括 2^80 以内）内，**Baillie-PSW 测试没有已知假阳性（伪素数）**。

**复杂度：**
- 时间复杂度（单次检测）：O(log² n)
- 空间复杂度：O(1)

## 算法实现

- 实现类：`BailliePSWPrimeChecker`
- 实现接口：`IPrimeChecker`

## 测试

```bash
dotnet test BailliePSW.Tests/BailliePSW.Tests.csproj
```
