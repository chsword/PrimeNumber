# 质数算法性能排行榜

> 最后更新：2026-05-25 01:13 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 12.91 |
| 🥈 #2 | 位压缩筛法 (Bitwise Sieve) | 13.45 |
| 🥉 #3 | 欧拉线性筛 (Linear Sieve) | 15.10 |
|    #4 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 15.40 |
|    #5 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 17.40 |
|    #6 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 17.86 |
|    #7 | 普里查德筛法 (Sieve of Pritchard) | 21.36 |
|    #8 | 并行分段筛法 (Parallel Segmented Sieve) | 22.63 |
|    #9 | 孙德兰筛法 (Sieve of Sundaram) | 25.17 |
|    #10 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 25.81 |
|    #11 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 34.08 |
|    #12 | 阿特金筛法 (Sieve of Atkin) | 46.91 |
|    #13 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 48.83 |
|    #14 | 分段筛法 (Segmented Sieve) | 49.07 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 173.18 |
|    #16 | 试除法 (Trial Division) | 364.69 |
|    #17 | Baillie-PSW 素性测试 | 381.68 |
|    #18 | 费马素性测试 (Fermat Primality Test) | 457.53 |
|    #19 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 508.77 |
|    #20 | 米勒-拉宾素性测试 (Miller-Rabin) | 594.07 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 1002.23 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
