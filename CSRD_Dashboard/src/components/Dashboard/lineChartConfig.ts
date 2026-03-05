import type { EChartsOption } from 'echarts'
import type { CallbackDataParams } from 'echarts/types/dist/shared'

interface TooltipParam {
  dataIndex: number
  value: number
  name: string
}

export const createLineChartConfig = (
  labels: string[],
  dataValues: number[],
  title: string,
  _onClickHandler?: (index: number) => void,
): EChartsOption => {
//   const minValue = Math.min(...dataValues)
//   const minIndex = dataValues.indexOf(minValue)

  const tooltipFormatter = (params: CallbackDataParams | CallbackDataParams[]): string => {
    const raw = Array.isArray(params) ? params[0] : params
    if (!raw) return ''
    const p = raw as unknown as TooltipParam
    const prev: number | undefined = p.dataIndex > 0 ? dataValues[p.dataIndex - 1] : undefined
    const diff: number | null = prev !== undefined ? p.value - prev : null
    const diffStr =
      diff !== null
        ? `<br/><span style="color:${diff < 0 ? '#ABD006' : '#FF6B6B'};font-size:12px">
                    ${diff < 0 ? '▼' : '▲'} ${Math.abs(diff).toLocaleString('nl-NL')} CO₂e/KG vs vorig jaar
                   </span>`
        : ''
    return `<div style="font-weight:600;margin-bottom:4px;color:#ABD006">${p.name}</div>
                 <div>${p.value.toLocaleString('nl-NL')} <span style="color:#8ABFB8">CO₂e/KG</span></div>
                 ${diffStr}`
  }

  return {
    title: {
      text: title,
      subtext: 'CO₂-equivalent reductie over de jaren',
      textStyle: {
        color: '#E7F9F7',
        fontSize: 16,
        fontWeight: 700,
        fontFamily: 'ui-monospace, monospace',
      },
      subtextStyle: {
        color: '#8ABFB8',
        fontSize: 11,
        fontFamily: 'ui-monospace, monospace',
      },
      top: 8,
      left: 8,
    },
    grid: {
      top: 72,
      left: 16,
      right: 24,
      bottom: 12,
      containLabel: true,
    },
    tooltip: {
      trigger: 'axis',
      backgroundColor: 'rgba(15, 30, 28, 0.92)',
      borderColor: '#ABD006',
      borderWidth: 1,
      borderRadius: 10,
      padding: [10, 14],
      textStyle: {
        color: '#E7F9F7',
        fontSize: 13,
        fontFamily: 'ui-monospace, monospace',
      },
      formatter: tooltipFormatter,
      axisPointer: {
        type: 'line',
        lineStyle: {
          color: '#ABD006',
          width: 1,
          type: 'dashed',
          opacity: 0.6,
        },
      },
    },
    xAxis: {
      type: 'category',
      data: labels,
      boundaryGap: false,
      axisLine: { lineStyle: { color: '#2A4A46' } },
      axisTick: { show: false },
      axisLabel: {
        color: '#8ABFB8',
        fontSize: 12,
        fontFamily: 'ui-monospace, monospace',
        fontWeight: 600,
      },
      splitLine: { show: false },
    },
    yAxis: {
      type: 'value',
      axisLine: { show: false },
      axisTick: { show: false },
      axisLabel: {
        color: '#8ABFB8',
        fontSize: 11,
        fontFamily: 'ui-monospace, monospace',
        formatter: '{value}',
      },
      splitLine: {
        lineStyle: {
          color: '#1E3A36',
          type: 'dashed',
          width: 1,
        },
      },
    },
    series: [
      {
        name: 'CO₂e/KG',
        type: 'line',
        data: dataValues,
        smooth: 0.4,
        symbol: 'circle',
        symbolSize: 8,
        showSymbol: true,
        lineStyle: {
          width: 3,
          color: {
            type: 'linear',
            x: 0,
            y: 0,
            x2: 1,
            y2: 0,
            colorStops: [
              { offset: 0, color: '#FF6B6B' },
              { offset: 0.5, color: '#F0C040' },
              { offset: 1, color: '#ABD006' },
            ],
          },
          shadowColor: 'rgba(171,208,6,0.25)',
          shadowBlur: 10,
        },
        itemStyle: {
          color: '#ABD006',
          borderColor: '#1A2E2B',
          borderWidth: 2,
        },
        emphasis: {
          itemStyle: {
            color: '#E7F9F7',
            borderColor: '#ABD006',
            borderWidth: 3,
            shadowColor: 'rgba(171,208,6,0.6)',
            shadowBlur: 12,
          },
          scale: true,
        },
        areaStyle: {
          color: {
            type: 'linear',
            x: 0,
            y: 0,
            x2: 0,
            y2: 1,
            colorStops: [
              { offset: 0, color: 'rgba(171,208,6,0.30)' },
              { offset: 0.6, color: 'rgba(171,208,6,0.08)' },
              { offset: 1, color: 'rgba(171,208,6,0.00)' },
            ],
          },
        },
        // markPoint: {
        //   data: [
        //     {
        //       name: 'Laagste uitstoot',
        //       coord: [minIndex, minValue],
        //       value: minValue,
        //       itemStyle: { color: '#ABD006' },
        //       label: {
        //         show: true,
        //         position: 'top',
        //         color: '#E7F9F7',
        //         fontSize: 11,
        //         fontFamily: 'ui-monospace, monospace',
        //         fontWeight: 700,
        //         formatter: '{c}',
        //       },
        //       symbol: 'pin',
        //       symbolSize: 36,
        //     },
        //   ],
        // },
        // markLine: {
        //   silent: true,
        //   data: [{ type: 'average' }],
        //   lineStyle: {
        //     color: '#8ABFB8',
        //     type: 'dashed',
        //     width: 1,
        //     opacity: 0.5,
        //   },
        //   label: {
        //     position: 'insideEndTop',
        //     color: '#8ABFB8',
        //     fontSize: 10,
        //     fontFamily: 'ui-monospace, monospace',
        //     formatter: 'Gem: {c}',
        //   },
        // },
      },
    ],
    animation: true,
    animationDuration: 1200,
    animationEasing: 'cubicOut',
  }
}
