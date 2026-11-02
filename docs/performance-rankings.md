# 质数算法性能排行榜

> 最后更新：2026-11-02 00:33 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 10.31 |
| 🥈 #2 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 12.02 |
| 🥉 #3 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 13.95 |
|    #4 | 普里查德筛法 (Sieve of Pritchard) | 16.41 |
|    #5 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 16.45 |
|    #6 | 位压缩筛法 (Bitwise Sieve) | 16.86 |
|    #7 | 并行分段筛法 (Parallel Segmented Sieve) | 18.21 |
|    #8 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 19.98 |
|    #9 | 孙德兰筛法 (Sieve of Sundaram) | 21.11 |
|    #10 | 欧拉线性筛 (Linear Sieve) | 21.90 |
|    #11 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 25.15 |
|    #12 | 分段筛法 (Segmented Sieve) | 26.27 |
|    #13 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 28.09 |
|    #14 | 阿特金筛法 (Sieve of Atkin) | 35.57 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 194.15 |
|    #16 | 试除法 (Trial Division) | 213.92 |
|    #17 | Baillie-PSW 素性测试 | 358.99 |
|    #18 | 费马素性测试 (Fermat Primality Test) | 385.88 |
|    #19 | 米勒-拉宾素性测试 (Miller-Rabin) | 527.51 |
|    #20 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 593.53 |
|    #21 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 756.58 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
