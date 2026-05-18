# 质数算法性能排行榜

> 最后更新：2026-05-18 01:10 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 13.26 |
| 🥈 #2 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 16.18 |
| 🥉 #3 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 17.89 |
|    #4 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 21.21 |
|    #5 | 普里查德筛法 (Sieve of Pritchard) | 22.57 |
|    #6 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 26.26 |
|    #7 | 并行分段筛法 (Parallel Segmented Sieve) | 26.30 |
|    #8 | 位压缩筛法 (Bitwise Sieve) | 28.28 |
|    #9 | 欧拉线性筛 (Linear Sieve) | 30.32 |
|    #10 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 34.83 |
|    #11 | 孙德兰筛法 (Sieve of Sundaram) | 34.93 |
|    #12 | 分段筛法 (Segmented Sieve) | 38.99 |
|    #13 | 阿特金筛法 (Sieve of Atkin) | 47.88 |
|    #14 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 63.64 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 165.02 |
|    #16 | 试除法 (Trial Division) | 343.59 |
|    #17 | Baillie-PSW 素性测试 | 428.20 |
|    #18 | 费马素性测试 (Fermat Primality Test) | 455.89 |
|    #19 | 米勒-拉宾素性测试 (Miller-Rabin) | 565.63 |
|    #20 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 708.92 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 940.30 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
