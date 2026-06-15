# 质数算法性能排行榜

> 最后更新：2026-06-15 01:23 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 13.38 |
| 🥈 #2 | 孙德兰筛法 (Sieve of Sundaram) | 13.96 |
| 🥉 #3 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 15.44 |
|    #4 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 17.91 |
|    #5 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 19.90 |
|    #6 | 普里查德筛法 (Sieve of Pritchard) | 20.33 |
|    #7 | 位压缩筛法 (Bitwise Sieve) | 23.87 |
|    #8 | 并行分段筛法 (Parallel Segmented Sieve) | 24.53 |
|    #9 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 25.97 |
|    #10 | 阿特金筛法 (Sieve of Atkin) | 26.00 |
|    #11 | 欧拉线性筛 (Linear Sieve) | 29.34 |
|    #12 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 35.66 |
|    #13 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 52.74 |
|    #14 | 分段筛法 (Segmented Sieve) | 63.41 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 253.50 |
|    #16 | 试除法 (Trial Division) | 309.09 |
|    #17 | Baillie-PSW 素性测试 | 422.33 |
|    #18 | 费马素性测试 (Fermat Primality Test) | 455.60 |
|    #19 | 米勒-拉宾素性测试 (Miller-Rabin) | 746.83 |
|    #20 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 780.89 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 926.81 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
