# 质数算法性能排行榜

> 最后更新：2026-08-03 00:51 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 13.56 |
| 🥈 #2 | 位压缩筛法 (Bitwise Sieve) | 14.59 |
| 🥉 #3 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 15.89 |
|    #4 | 欧拉线性筛 (Linear Sieve) | 16.47 |
|    #5 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 17.91 |
|    #6 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 19.68 |
|    #7 | 普里查德筛法 (Sieve of Pritchard) | 22.85 |
|    #8 | 并行分段筛法 (Parallel Segmented Sieve) | 26.37 |
|    #9 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 26.89 |
|    #10 | 孙德兰筛法 (Sieve of Sundaram) | 27.52 |
|    #11 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 35.83 |
|    #12 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 36.98 |
|    #13 | 分段筛法 (Segmented Sieve) | 37.70 |
|    #14 | 阿特金筛法 (Sieve of Atkin) | 47.31 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 183.74 |
|    #16 | 试除法 (Trial Division) | 290.16 |
|    #17 | 费马素性测试 (Fermat Primality Test) | 396.55 |
|    #18 | Baillie-PSW 素性测试 | 415.54 |
|    #19 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 510.25 |
|    #20 | 米勒-拉宾素性测试 (Miller-Rabin) | 521.08 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 833.06 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
