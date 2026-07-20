# 质数算法性能排行榜

> 最后更新：2026-07-20 03:02 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 13.89 |
| 🥈 #2 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 16.57 |
| 🥉 #3 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 18.16 |
|    #4 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 20.02 |
|    #5 | 普里查德筛法 (Sieve of Pritchard) | 23.21 |
|    #6 | 孙德兰筛法 (Sieve of Sundaram) | 26.94 |
|    #7 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 27.22 |
|    #8 | 并行分段筛法 (Parallel Segmented Sieve) | 27.68 |
|    #9 | 欧拉线性筛 (Linear Sieve) | 28.54 |
|    #10 | 位压缩筛法 (Bitwise Sieve) | 29.86 |
|    #11 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 36.51 |
|    #12 | 阿特金筛法 (Sieve of Atkin) | 44.25 |
|    #13 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 61.00 |
|    #14 | 分段筛法 (Segmented Sieve) | 88.29 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 239.51 |
|    #16 | 试除法 (Trial Division) | 354.43 |
|    #17 | 费马素性测试 (Fermat Primality Test) | 409.96 |
|    #18 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 481.22 |
|    #19 | Baillie-PSW 素性测试 | 525.75 |
|    #20 | 米勒-拉宾素性测试 (Miller-Rabin) | 532.63 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 854.77 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
