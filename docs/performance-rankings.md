# 质数算法性能排行榜

> 最后更新：2026-03-15 12:40 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 10.81 |
| 🥈 #2 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 11.27 |
| 🥉 #3 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 11.62 |
|    #4 | 普里查德筛法 (Sieve of Pritchard) | 14.60 |
|    #5 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 16.69 |
|    #6 | 并行分段筛法 (Parallel Segmented Sieve) | 17.80 |
|    #7 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 21.60 |
|    #8 | 欧拉线性筛 (Linear Sieve) | 28.17 |
|    #9 | 孙德兰筛法 (Sieve of Sundaram) | 29.02 |
|    #10 | 位压缩筛法 (Bitwise Sieve) | 29.68 |
|    #11 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 29.97 |
|    #12 | 阿特金筛法 (Sieve of Atkin) | 47.80 |
|    #13 | 分段筛法 (Segmented Sieve) | 72.29 |
|    #14 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 214.13 |
|    #15 | 试除法 (Trial Division) | 245.58 |
|    #16 | 费马素性测试 (Fermat Primality Test) | 383.11 |
|    #17 | Baillie-PSW 素性测试 | 447.79 |
|    #18 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 468.83 |
|    #19 | 米勒-拉宾素性测试 (Miller-Rabin) | 542.52 |
|    #20 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 594.84 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
