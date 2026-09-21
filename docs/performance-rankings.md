# 质数算法性能排行榜

> 最后更新：2026-09-21 00:54 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 9.15 |
| 🥈 #2 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 10.83 |
| 🥉 #3 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 11.88 |
|    #4 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 12.10 |
|    #5 | 孙德兰筛法 (Sieve of Sundaram) | 15.05 |
|    #6 | 并行分段筛法 (Parallel Segmented Sieve) | 19.05 |
|    #7 | 普里查德筛法 (Sieve of Pritchard) | 22.42 |
|    #8 | 阿特金筛法 (Sieve of Atkin) | 26.20 |
|    #9 | 位压缩筛法 (Bitwise Sieve) | 27.02 |
|    #10 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 27.20 |
|    #11 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 28.25 |
|    #12 | 欧拉线性筛 (Linear Sieve) | 29.40 |
|    #13 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 34.19 |
|    #14 | 分段筛法 (Segmented Sieve) | 70.01 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 210.74 |
|    #16 | 试除法 (Trial Division) | 266.52 |
|    #17 | Baillie-PSW 素性测试 | 407.46 |
|    #18 | 费马素性测试 (Fermat Primality Test) | 433.70 |
|    #19 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 478.89 |
|    #20 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 608.32 |
|    #21 | 米勒-拉宾素性测试 (Miller-Rabin) | 625.03 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
