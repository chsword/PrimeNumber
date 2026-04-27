# 质数算法性能排行榜

> 最后更新：2026-04-27 01:00 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 位压缩筛法 (Bitwise Sieve) | 9.45 |
| 🥈 #2 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 9.50 |
| 🥉 #3 | 欧拉线性筛 (Linear Sieve) | 10.46 |
|    #4 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 11.13 |
|    #5 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 11.66 |
|    #6 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 11.67 |
|    #7 | 孙德兰筛法 (Sieve of Sundaram) | 14.46 |
|    #8 | 普里查德筛法 (Sieve of Pritchard) | 15.96 |
|    #9 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 17.21 |
|    #10 | 并行分段筛法 (Parallel Segmented Sieve) | 17.99 |
|    #11 | 分段筛法 (Segmented Sieve) | 21.40 |
|    #12 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 21.80 |
|    #13 | 阿特金筛法 (Sieve of Atkin) | 24.10 |
|    #14 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 32.30 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 152.69 |
|    #16 | 试除法 (Trial Division) | 194.88 |
|    #17 | Baillie-PSW 素性测试 | 286.52 |
|    #18 | 费马素性测试 (Fermat Primality Test) | 336.60 |
|    #19 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 384.56 |
|    #20 | 米勒-拉宾素性测试 (Miller-Rabin) | 431.41 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 649.28 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
