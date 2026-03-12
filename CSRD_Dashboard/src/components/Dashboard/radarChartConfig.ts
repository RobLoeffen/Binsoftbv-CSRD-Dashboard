import type { EChartsOption } from 'echarts'

export const createRadarChartConfig = (
  labels: string[],
  dataValues: number[],
  title: string,
  _onClickHandler?: (index: number) => void,
): EChartsOption => {
  const overall = Math.round(dataValues.reduce((sum, value) => sum + value, 0) / dataValues.length)

  return {
    title: {
      text: title,
      subtext: `Voortgang: ${overall}% voltooid`,
      textStyle: {
        color: '#E7F9F7',
        fontSize: 16,
        fontWeight: 700,
        fontFamily: 'ui-monospace, monospace',
      },
      subtextStyle: {
        color: overall >= 80 ? '#ABD006' : overall >= 50 ? '#FCFF66' : '#FF967E',
        fontSize: 12,
        fontFamily: 'ui-monospace, monospace',
        fontWeight: 700,
      },
      top: 8,
      left: 8,
    },
    radar: {
      indicator: labels.map((label) => ({ name: label, max: 100 })),
      shape: 'polygon',
      center: ['50%', '55%'],
      radius: '70%',
      splitNumber: 4,
      axisName: {
        color: '#8ABFB8',
        fontSize: 11,
        fontFamily: 'ui-monospace, monospace',
        formatter: (name?: string) => {
          if (!name) return ''
          const index = labels.indexOf(name)
          const value = dataValues[index] ?? 0
          return `{label|${name}}\n{pct|${value}%}`
        },
        rich: {
          label: {
            color: '#8ABFB8',
            fontSize: 11,
            fontFamily: 'ui-monospace, monospace',
            align: 'center',
          },
          pct: {
            color: '#E7F9F7',
            fontSize: 12,
            fontWeight: 700,
            fontFamily: 'ui-monospace, monospace',
            align: 'center',
          },
        },
      },
      splitLine: {
        lineStyle: { color: '#2A4A46', type: 'dashed', width: 1 },
      },
      splitArea: {
        show: true,
        areaStyle: {
          color: [
            'rgba(171,208,6,0.02)',
            'rgba(171,208,6,0.04)',
            'rgba(171,208,6,0.06)',
            'rgba(171,208,6,0.08)',
          ],
        },
      },
      axisLine: {
        lineStyle: { color: '#2A4A46', width: 1 },
      },
    },
    series: [
      {
        name: title,
        type: 'radar',
        data: [
          {
            value: dataValues,
            name: 'Voortgang',
            lineStyle: {
              color: '#ABD006',
              width: 2,
            },
            itemStyle: {
              color: '#ABD006',
              borderColor: '#1A2E2B',
              borderWidth: 2,
            },
            areaStyle: {
              color: {
                type: 'radial' as const,
                x: 0.5,
                y: 0.5,
                r: 0.8,
                colorStops: [
                  { offset: 0, color: 'rgba(171,208,6,0.50)' },
                  { offset: 1, color: 'rgba(171,208,6,0.10)' },
                ],
              },
            },
          },
        ],
        animationDuration: 1200,
        animationEasing: 'cubicOut',
      },
    ] as EChartsOption['series'],
  }
}
