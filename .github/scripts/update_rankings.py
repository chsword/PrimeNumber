#!/usr/bin/env python3
"""
update_rankings.py
根据最新基准测试输出，更新 docs/performance-rankings.md 中的性能排行榜。
"""

import argparse
import re
import os
from datetime import datetime, timezone


def parse_args():
    parser = argparse.ArgumentParser(description="更新性能排行榜")
    group = parser.add_mutually_exclusive_group(required=True)
    group.add_argument("--output", help="基准测试输出内容（文本）")
    group.add_argument("--output-file", help="包含基准测试输出的文件路径")
    parser.add_argument("--rankings-file", required=True, help="排行榜 Markdown 文件路径")
    return parser.parse_args()


def parse_results(output: str) -> list[tuple[str, float]]:
    """解析算法名称和耗时（毫秒），返回 (name, ms) 列表（未排序）。"""
    results = []
    for line in output.splitlines():
        line = line.strip()
        if line.startswith("#") or not line:
            continue
        parts = line.split("\t")
        if len(parts) == 2:
            name = parts[0]
            try:
                ms = float(parts[1])
                results.append((name, ms))
            except ValueError:
                pass
    return results


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


def build_markdown(rankings: list[tuple[int, str, float]]) -> str:
    now = datetime.now(timezone.utc).strftime("%Y-%m-%d %H:%M UTC")
    lines = [
        "# 质数算法性能排行榜",
        "",
        f"> 最后更新：{now}",
        f"> 基准测试：获取不超过 1,000,000 的所有质数（中位数耗时，单位 ms）",
        "",
        "| 排名 | 算法名称 | 中位耗时 (ms) |",
        "| ---- | -------- | ------------- |",
    ]
    for rank, name, ms in rankings:
        medal = {1: "🥇", 2: "🥈", 3: "🥉"}.get(rank, "  ")
        lines.append(f"| {medal} #{rank} | {name} | {ms:.2f} |")

    lines += [
        "",
        "---",
        "",
        "## 说明",
        "",
        "- 排名在每次 PR 提交的 CI 验证时及每周一（UTC 00:00）自动更新。",
        "- 仅性能进入前 10 的算法可合并至主分支。",
        "- 超出第 30 名的算法将在下次定期清理时被移除。",
    ]
    return "\n".join(lines) + "\n"


def main():
    args = parse_args()
    if args.output_file:
        with open(args.output_file, encoding="utf-8") as f:
            output = f.read()
    else:
        output = args.output
    rankings = parse_rankings(output)

    if not rankings:
        print("⚠️  未找到排名信息，跳过更新。")
        return

    markdown = build_markdown(rankings)

    os.makedirs(os.path.dirname(os.path.abspath(args.rankings_file)), exist_ok=True)
    with open(args.rankings_file, "w", encoding="utf-8") as f:
        f.write(markdown)

    print(f"✅ 排行榜已更新至 {args.rankings_file}（共 {len(rankings)} 个算法）")


if __name__ == "__main__":
    main()
