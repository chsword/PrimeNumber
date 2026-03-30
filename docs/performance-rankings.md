# 质数算法性能排行榜

> 最后更新：2026-03-30 00:53 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 9.03 |
| 🥈 #2 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 10.56 |
| 🥉 #3 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 11.74 |
|    #4 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 12.03 |
|    #5 | 位压缩筛法 (Bitwise Sieve) | 15.29 |
|    #6 | 欧拉线性筛 (Linear Sieve) | 15.68 |
|    #7 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 16.72 |
|    #8 | 并行分段筛法 (Parallel Segmented Sieve) | 17.31 |
|    #9 | 普里查德筛法 (Sieve of Pritchard) | 18.21 |
|    #10 | 孙德兰筛法 (Sieve of Sundaram) | 20.23 |
|    #11 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 22.12 |
|    #12 | 阿特金筛法 (Sieve of Atkin) | 28.09 |
|    #13 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 32.61 |
|    #14 | 分段筛法 (Segmented Sieve) | 37.89 |
|    #15 | 试除法 (Trial Division) | 178.02 |
|    #16 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 181.34 |
|    #17 | 费马素性测试 (Fermat Primality Test) | 331.85 |
|    #18 | Baillie-PSW 素性测试 | 386.21 |
|    #19 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 475.37 |
|    #20 | 米勒-拉宾素性测试 (Miller-Rabin) | 544.08 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 595.58 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
