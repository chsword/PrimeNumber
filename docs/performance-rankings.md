# 质数算法性能排行榜

> 最后更新：2026-11-09 00:33 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 5.51 |
| 🥈 #2 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 6.40 |
| 🥉 #3 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 6.75 |
|    #4 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 8.40 |
|    #5 | 孙德兰筛法 (Sieve of Sundaram) | 8.49 |
|    #6 | 普里查德筛法 (Sieve of Pritchard) | 8.68 |
|    #7 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 9.82 |
|    #8 | 并行分段筛法 (Parallel Segmented Sieve) | 11.19 |
|    #9 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 12.12 |
|    #10 | 欧拉线性筛 (Linear Sieve) | 12.70 |
|    #11 | 位压缩筛法 (Bitwise Sieve) | 15.89 |
|    #12 | 分段筛法 (Segmented Sieve) | 19.81 |
|    #13 | 阿特金筛法 (Sieve of Atkin) | 26.91 |
|    #14 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 34.56 |
|    #15 | 试除法 (Trial Division) | 148.28 |
|    #16 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 156.92 |
|    #17 | 费马素性测试 (Fermat Primality Test) | 267.68 |
|    #18 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 313.09 |
|    #19 | Baillie-PSW 素性测试 | 318.94 |
|    #20 | 米勒-拉宾素性测试 (Miller-Rabin) | 321.16 |
|    #21 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 471.89 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
