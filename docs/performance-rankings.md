# 质数算法性能排行榜

> 最后更新：2026-07-13 00:51 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 12.88 |
| 🥈 #2 | 位压缩筛法 (Bitwise Sieve) | 14.16 |
| 🥉 #3 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 15.28 |
|    #4 | 欧拉线性筛 (Linear Sieve) | 17.44 |
|    #5 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 17.71 |
|    #6 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 17.74 |
|    #7 | 普里查德筛法 (Sieve of Pritchard) | 21.10 |
|    #8 | 并行分段筛法 (Parallel Segmented Sieve) | 22.96 |
|    #9 | 孙德兰筛法 (Sieve of Sundaram) | 23.73 |
|    #10 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 25.82 |
|    #11 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 33.86 |
|    #12 | 阿特金筛法 (Sieve of Atkin) | 40.48 |
|    #13 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 58.22 |
|    #14 | 分段筛法 (Segmented Sieve) | 63.22 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 147.27 |
|    #16 | 试除法 (Trial Division) | 278.62 |
|    #17 | Baillie-PSW 素性测试 | 392.29 |
|    #18 | 费马素性测试 (Fermat Primality Test) | 455.87 |
|    #19 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 474.41 |
|    #20 | 米勒-拉宾素性测试 (Miller-Rabin) | 553.30 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 927.16 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
