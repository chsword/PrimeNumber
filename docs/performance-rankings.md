# 质数算法性能排行榜

> 最后更新：2026-08-17 00:21 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 普里查德筛法 (Sieve of Pritchard) | 13.52 |
| 🥈 #2 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 13.74 |
| 🥉 #3 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 15.76 |
|    #4 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 17.56 |
|    #5 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 19.13 |
|    #6 | 位压缩筛法 (Bitwise Sieve) | 25.77 |
|    #7 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 26.48 |
|    #8 | 并行分段筛法 (Parallel Segmented Sieve) | 27.31 |
|    #9 | 欧拉线性筛 (Linear Sieve) | 28.32 |
|    #10 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 38.93 |
|    #11 | 孙德兰筛法 (Sieve of Sundaram) | 41.71 |
|    #12 | 阿特金筛法 (Sieve of Atkin) | 50.61 |
|    #13 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 64.44 |
|    #14 | 分段筛法 (Segmented Sieve) | 66.38 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 220.31 |
|    #16 | 试除法 (Trial Division) | 353.41 |
|    #17 | 费马素性测试 (Fermat Primality Test) | 411.52 |
|    #18 | Baillie-PSW 素性测试 | 475.16 |
|    #19 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 556.89 |
|    #20 | 米勒-拉宾素性测试 (Miller-Rabin) | 637.93 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 824.86 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
