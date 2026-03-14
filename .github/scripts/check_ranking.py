#!/usr/bin/env python3
"""
check_ranking.py
检查基准测试输出中，新算法的排名是否在前 N 名之内。
若排名超出阈值则以非零退出码退出，使工作流失败（阻止合并）。

输出格式（来自 SimpleRunner）：
  AlgorithmName<TAB>MedianMs
  ...
  # Ranking (fastest first):
  #1<TAB>AlgorithmName<TAB>xxms
  #2<TAB>AlgorithmName<TAB>xxms
"""

import argparse
import sys
import re


def parse_args():
    parser = argparse.ArgumentParser(description="检查算法排名")
    parser.add_argument("--output", required=True, help="基准测试输出内容")
    parser.add_argument("--top", type=int, default=10, help="允许合并的最大排名（默认 10）")
    return parser.parse_args()


def parse_rankings(output: str) -> list[tuple[int, str, float]]:
    """解析排名行，返回 (rank, name, ms) 列表。"""
    rankings = []
    in_ranking = False
    for line in output.splitlines():
        line = line.strip()
        if line.startswith("# Ranking"):
            in_ranking = True
            continue
        if in_ranking and re.match(r"^#\d+\t", line):
            parts = line.split("\t")
            if len(parts) >= 3:
                rank = int(parts[0].lstrip("#"))
                name = parts[1]
                ms_str = parts[2].rstrip("ms")
                try:
                    ms = float(ms_str)
                except ValueError:
                    ms = 0.0
                rankings.append((rank, name, ms))
    return rankings


def main():
    args = parse_args()
    rankings = parse_rankings(args.output)

    if not rankings:
        print("⚠️  未找到排名信息，跳过排名检查。")
        sys.exit(0)

    print(f"\n📊 当前性能排名（前 {args.top} 名可合并）：")
    for rank, name, ms in rankings:
        status = "✅" if rank <= args.top else "❌"
        print(f"  {status} #{rank} {name} ({ms:.2f}ms)")

    # 检查排名总数是否超过阈值
    total = len(rankings)
    if total <= args.top:
        print(f"\n✅ 算法总数 ({total}) 未超过前 {args.top} 名，允许合并。")
        sys.exit(0)

    # 找出最后一个算法（假设 PR 新增的算法排在列表末尾）
    # 实际上，所有算法都重新排序，检查是否有算法排在 top 之外
    out_of_top = [r for r in rankings if r[0] > args.top]
    if out_of_top:
        names = ", ".join(r[1] for r in out_of_top)
        print(f"\n❌ 以下算法排名超出前 {args.top} 名，不允许合并：{names}")
        print("   请优化算法性能后重新提交 PR。")
        sys.exit(1)

    print(f"\n✅ 所有算法均在前 {args.top} 名之内，允许合并。")
    sys.exit(0)


if __name__ == "__main__":
    main()
