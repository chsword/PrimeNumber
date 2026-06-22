# 质数算法性能排行榜

> 最后更新：2026-06-22 01:22 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 13.03 |
| 🥈 #2 | 位压缩筛法 (Bitwise Sieve) | 13.84 |
| 🥉 #3 | 孙德兰筛法 (Sieve of Sundaram) | 14.43 |
|    #4 | 欧拉线性筛 (Linear Sieve) | 15.31 |
|    #5 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 15.41 |
|    #6 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 17.46 |
|    #7 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 18.79 |
|    #8 | 普里查德筛法 (Sieve of Pritchard) | 21.42 |
|    #9 | 并行分段筛法 (Parallel Segmented Sieve) | 23.47 |
|    #10 | 阿特金筛法 (Sieve of Atkin) | 23.74 |
|    #11 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 26.01 |
|    #12 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 32.89 |
|    #13 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 34.27 |
|    #14 | 分段筛法 (Segmented Sieve) | 34.76 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 179.46 |
|    #16 | 试除法 (Trial Division) | 180.58 |
|    #17 | Baillie-PSW 素性测试 | 370.80 |
|    #18 | 米勒-拉宾素性测试 (Miller-Rabin) | 449.99 |
|    #19 | 费马素性测试 (Fermat Primality Test) | 453.11 |
|    #20 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 480.40 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 951.97 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
