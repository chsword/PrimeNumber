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
    group = parser.add_mutually_exclusive_group(required=True)
    group.add_argument("--output", help="基准测试输出内容（文本）")
    group.add_argument("--output-file", help="包含基准测试输出的文件路径")
    parser.add_argument("--top", type=int, default=10, help="允许合并的最大排名（默认 10）")
    parser.add_argument(
        "--new-algorithm",
        help="新增算法的目录名（如 sieve-of-eratosthenes）。"
             "若提供，则仅检查该算法的排名；未匹配时回退为全量检查。",
    )
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


def normalize_name(s: str) -> str:
    """统一化名称：转小写，连字符和下划线替换为空格。"""
    return s.lower().replace("-", " ").replace("_", " ")


def find_new_algorithm(
    rankings: list[tuple[int, str, float]], dir_name: str
) -> list[tuple[int, str, float]]:
    """
    在排名列表中模糊匹配由目录名标识的新算法。
    目录名（如 "sieve-of-eratosthenes"）与算法展示名（如
    "埃拉托色尼筛法 (Sieve of Eratosthenes)"）之间通过提取括号内的
    英文部分进行不区分大小写的子串匹配。
    """
    needle = normalize_name(dir_name)
    matched = []
    for rank, name, ms in rankings:
        # 先尝试匹配括号内的英文名，再回退到全名匹配
        eng_match = re.search(r"\(([^)]+)\)", name)
        candidates = []
        if eng_match:
            candidates.append(normalize_name(eng_match.group(1)))
        candidates.append(normalize_name(name))
        if any(needle in c for c in candidates):
            matched.append((rank, name, ms))
    return matched


def main():
    args = parse_args()
    if args.output_file:
        with open(args.output_file, encoding="utf-8") as f:
            output = f.read()
    else:
        output = args.output
    rankings = parse_rankings(output)

    if not rankings:
        print("⚠️  未找到排名信息，跳过排名检查。")
        sys.exit(0)

    print(f"\n📊 当前性能排名（前 {args.top} 名可合并）：")
    for rank, name, ms in rankings:
        status = "✅" if rank <= args.top else "❌"
        print(f"  {status} #{rank} {name} ({ms:.2f}ms)")

    # ── 指定新算法时：仅检查该算法的排名 ──────────────────────────────
    if args.new_algorithm:
        matched = find_new_algorithm(rankings, args.new_algorithm)
        if matched:
            failed = [r for r in matched if r[0] > args.top]
            passed = [r for r in matched if r[0] <= args.top]
            for rank, name, ms in passed:
                print(f"\n✅ 新算法 {name} 排名第 {rank}，允许合并。")
            for rank, name, ms in failed:
                print(f"\n❌ 新算法 {name} 排名第 {rank}，超出前 {args.top} 名，不允许合并。")
            if failed:
                print("   请优化算法性能后重新提交 PR。")
                sys.exit(1)
            sys.exit(0)
        else:
            print(
                f"\n⚠️  未找到与目录名 {args.new_algorithm!r} 匹配的算法，"
                "回退为全量排名检查。"
            )
            # fall through to total-count check below

    # ── 全量检查：算法总数超过阈值时，检查是否有算法排在 top 之外 ──────
    total = len(rankings)
    if total <= args.top:
        print(f"\n✅ 算法总数 ({total}) 未超过前 {args.top} 名，允许合并。")
        sys.exit(0)

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
