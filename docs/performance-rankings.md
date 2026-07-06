# 质数算法性能排行榜

> 最后更新：2026-07-06 01:00 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 13.94 |
| 🥈 #2 | 位压缩筛法 (Bitwise Sieve) | 15.11 |
| 🥉 #3 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 16.16 |
|    #4 | 欧拉线性筛 (Linear Sieve) | 16.48 |
|    #5 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 18.26 |
|    #6 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 20.48 |
|    #7 | 普里查德筛法 (Sieve of Pritchard) | 22.96 |
|    #8 | 并行分段筛法 (Parallel Segmented Sieve) | 25.32 |
|    #9 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 27.09 |
|    #10 | 孙德兰筛法 (Sieve of Sundaram) | 27.88 |
|    #11 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 34.36 |
|    #12 | 分段筛法 (Segmented Sieve) | 38.14 |
|    #13 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 39.40 |
|    #14 | 阿特金筛法 (Sieve of Atkin) | 45.69 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 168.06 |
|    #16 | 试除法 (Trial Division) | 227.13 |
|    #17 | Baillie-PSW 素性测试 | 304.09 |
|    #18 | 费马素性测试 (Fermat Primality Test) | 430.92 |
|    #19 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 468.45 |
|    #20 | 米勒-拉宾素性测试 (Miller-Rabin) | 495.88 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 953.47 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
