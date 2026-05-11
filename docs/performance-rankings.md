# 质数算法性能排行榜

> 最后更新：2026-05-11 01:07 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 9.51 |
| 🥈 #2 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 11.13 |
| 🥉 #3 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 11.68 |
|    #4 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 11.91 |
|    #5 | 普里查德筛法 (Sieve of Pritchard) | 14.25 |
|    #6 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 16.91 |
|    #7 | 孙德兰筛法 (Sieve of Sundaram) | 17.80 |
|    #8 | 并行分段筛法 (Parallel Segmented Sieve) | 17.94 |
|    #9 | 位压缩筛法 (Bitwise Sieve) | 20.90 |
|    #10 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 21.52 |
|    #11 | 阿特金筛法 (Sieve of Atkin) | 24.30 |
|    #12 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 33.11 |
|    #13 | 欧拉线性筛 (Linear Sieve) | 33.29 |
|    #14 | 分段筛法 (Segmented Sieve) | 61.40 |
|    #15 | 试除法 (Trial Division) | 183.72 |
|    #16 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 225.78 |
|    #17 | 费马素性测试 (Fermat Primality Test) | 340.26 |
|    #18 | Baillie-PSW 素性测试 | 370.10 |
|    #19 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 484.98 |
|    #20 | 米勒-拉宾素性测试 (Miller-Rabin) | 494.16 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 655.58 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
