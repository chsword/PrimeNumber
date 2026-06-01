# 质数算法性能排行榜

> 最后更新：2026-06-01 01:19 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 9.85 |
| 🥈 #2 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 11.34 |
| 🥉 #3 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 11.50 |
|    #4 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 11.89 |
|    #5 | 位压缩筛法 (Bitwise Sieve) | 14.53 |
|    #6 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 17.38 |
|    #7 | 并行分段筛法 (Parallel Segmented Sieve) | 19.15 |
|    #8 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 21.88 |
|    #9 | 普里查德筛法 (Sieve of Pritchard) | 22.03 |
|    #10 | 欧拉线性筛 (Linear Sieve) | 28.96 |
|    #11 | 孙德兰筛法 (Sieve of Sundaram) | 33.25 |
|    #12 | 分段筛法 (Segmented Sieve) | 35.51 |
|    #13 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 36.51 |
|    #14 | 阿特金筛法 (Sieve of Atkin) | 71.65 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 157.86 |
|    #16 | 试除法 (Trial Division) | 190.83 |
|    #17 | Baillie-PSW 素性测试 | 370.11 |
|    #18 | 费马素性测试 (Fermat Primality Test) | 421.89 |
|    #19 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 471.58 |
|    #20 | 米勒-拉宾素性测试 (Miller-Rabin) | 545.29 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 770.13 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
