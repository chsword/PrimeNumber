# 质数算法性能排行榜

> 最后更新：2026-10-19 00:50 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 10.59 |
| 🥈 #2 | 位压缩筛法 (Bitwise Sieve) | 10.96 |
| 🥉 #3 | 孙德兰筛法 (Sieve of Sundaram) | 11.36 |
|    #4 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 11.92 |
|    #5 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 13.01 |
|    #6 | 欧拉线性筛 (Linear Sieve) | 13.06 |
|    #7 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 14.43 |
|    #8 | 普里查德筛法 (Sieve of Pritchard) | 17.17 |
|    #9 | 阿特金筛法 (Sieve of Atkin) | 17.87 |
|    #10 | 并行分段筛法 (Parallel Segmented Sieve) | 19.74 |
|    #11 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 19.88 |
|    #12 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 23.07 |
|    #13 | 分段筛法 (Segmented Sieve) | 26.94 |
|    #14 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 27.09 |
|    #15 | 试除法 (Trial Division) | 174.35 |
|    #16 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 178.03 |
|    #17 | 费马素性测试 (Fermat Primality Test) | 341.80 |
|    #18 | Baillie-PSW 素性测试 | 456.91 |
|    #19 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 546.14 |
|    #20 | 米勒-拉宾素性测试 (Miller-Rabin) | 575.94 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 663.16 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
