# 质数算法性能排行榜

> 最后更新：2026-08-31 00:55 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 13.48 |
| 🥈 #2 | 位压缩筛法 (Bitwise Sieve) | 14.71 |
| 🥉 #3 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 15.91 |
|    #4 | 欧拉线性筛 (Linear Sieve) | 16.64 |
|    #5 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 17.59 |
|    #6 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 20.64 |
|    #7 | 普里查德筛法 (Sieve of Pritchard) | 21.11 |
|    #8 | 孙德兰筛法 (Sieve of Sundaram) | 26.51 |
|    #9 | 并行分段筛法 (Parallel Segmented Sieve) | 26.60 |
|    #10 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 26.74 |
|    #11 | 分段筛法 (Segmented Sieve) | 31.85 |
|    #12 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 38.22 |
|    #13 | 阿特金筛法 (Sieve of Atkin) | 40.80 |
|    #14 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 42.79 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 179.03 |
|    #16 | 试除法 (Trial Division) | 338.05 |
|    #17 | Baillie-PSW 素性测试 | 377.76 |
|    #18 | 费马素性测试 (Fermat Primality Test) | 394.21 |
|    #19 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 473.97 |
|    #20 | 米勒-拉宾素性测试 (Miller-Rabin) | 567.65 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 828.25 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
