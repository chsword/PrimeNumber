# 质数算法性能排行榜

> 最后更新：2026-06-29 01:15 UTC
> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）

| 排名 | 算法名称 | 中位耗时 (ms) |
| ---- | -------- | ------------- |
| 🥇 #1 | 210-轮位压缩筛 (Wheel-210 Bitwise Sieve) | 7.69 |
| 🥈 #2 | 扎基亚质数筛法 (Sieve of Zakiya, SoZ7) | 9.27 |
| 🥉 #3 | 位压缩分段筛法 (Bitwise Segmented Sieve) | 9.31 |
|    #4 | 增强轮筛法 (Enhanced Wheel Sieve, 30030-Wheel) | 9.55 |
|    #5 | 普里查德筛法 (Sieve of Pritchard) | 10.67 |
|    #6 | 30-轮分段筛法 (Wheel-30 Segmented Sieve) | 13.80 |
|    #7 | 并行分段筛法 (Parallel Segmented Sieve) | 15.47 |
|    #8 | 欧拉线性分段筛法 (Euler Linear Segmented Sieve) | 17.26 |
|    #9 | 位压缩筛法 (Bitwise Sieve) | 19.84 |
|    #10 | 孙德兰筛法 (Sieve of Sundaram) | 20.09 |
|    #11 | 欧拉线性筛 (Linear Sieve) | 27.36 |
|    #12 | 分段筛法 (Segmented Sieve) | 28.25 |
|    #13 | 阿特金筛法 (Sieve of Atkin) | 37.44 |
|    #14 | 埃拉托色尼筛法 (Sieve of Eratosthenes) | 46.40 |
|    #15 | 轮式因式分解法 (Wheel Factorization, Wheel-30) | 244.97 |
|    #16 | 试除法 (Trial Division) | 342.78 |
|    #17 | 二次弗罗贝尼乌斯测试 (Quadratic Frobenius Test) | 369.46 |
|    #18 | Baillie-PSW 素性测试 | 395.93 |
|    #19 | 费马素性测试 (Fermat Primality Test) | 435.03 |
|    #20 | 索洛维-斯特拉森素性测试 (Solovay-Strassen) | 596.03 |
|    #21 | 米勒-拉宾素性测试 (Miller-Rabin) | 653.50 |

---

## 说明

- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。
- 仅性能进入前 10 的算法可合并至主分支。
- 超出第 30 名的算法将在下次定期清理时被移除。
