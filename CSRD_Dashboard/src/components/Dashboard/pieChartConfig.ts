import type { EChartsOption } from 'echarts'

const getColorByValue = (value: number): string => {
  if (value > 75) return '#ABD006'
  if (value >= 40) return '#FCFF66'
  if (value >= 1) return '#FF967E'
  return '#D6F5F2'
}

export const createPieChartConfig = (
  labels: string[],
  dataValues: number[],
  title: string,
  _onClickHandler?: (index: number) => void,
): EChartsOption => {
  return {
    title: {
      text: title,
      subtext: 'Verdeling voortgang per fase',
      left: 'center',
      top: 8,
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
    },
    tooltip: {
      trigger: 'item',
      formatter: '{b} : {c} ({d}%)',
      backgroundColor: 'rgba(15, 30, 28, 0.92)',
      borderWidth: 1,
      textStyle: {
        color: '#E7F9F7',
        fontSize: 13,
        fontFamily: 'ui-monospace, monospace',
      },
    },
    legend: {
      orient: 'horizontal',
      bottom: 'bottom',
      textStyle: {
        color: '#8ABFB8',
        fontFamily: 'ui-monospace, monospace',
        fontSize: 11, 
      },
    },
    series: [
      {
        name: title,
        type: 'pie',
        radius: ['42%', '70%'],
        center: ['50%', '52%'],
        avoidLabelOverlap: true,
        padAngle: 3,
        itemStyle: {
          borderRadius: 6,
          borderColor: 'rgba(15, 30, 28, 0.8)',
          borderWidth: 2,
        },
        label: {
          show: true,
          position: 'outside',
          color: '#8ABFB8',
          fontSize: 11,
          fontFamily: 'ui-monospace, monospace',
          formatter: '{b}\n{d}%',
          lineHeight: 16,
        },
        data: labels.map((label, i) => {
          const color = getColorByValue(dataValues[i] ?? 0)
          return {
            name: label,
            value: dataValues[i] ?? 0,
            itemStyle: { color },
            emphasis: {
              itemStyle: {
                shadowBlur: 24,
                shadowOffsetX: 0,
                shadowColor: color,
                borderWidth: 3,
              },
            },
          }
        }),
        emphasis: {
          scale: true,
          scaleSize: 8,
          label: {
            show: true,
            fontSize: 13,
            fontWeight: 700,
            color: '#E7F9F7',
          },
        },
      },
    ],
    animation: true,
    animationDuration: 1000,
    animationEasing: 'cubicOut',
  }
}
