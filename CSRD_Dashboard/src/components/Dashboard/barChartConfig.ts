import type { EChartsOption } from "echarts"

const getColorByValue = (value: number): string => {
  if (value > 75) return '#ABD006'
  if (value >= 40) return '#FCFF66'
  if (value >= 1) return '#FF967E'
  return '#D6F5F2'
}

export const createBarChartConfig = (
  labels: string[],
  dataValues: number[],
  title: string,
  datasetLabel?: string,
  onClickHandler?: (index: number) => void,
): EChartsOption => {
   return {
    title: { text: title, textStyle: { color: '#E7F9F7', fontSize: 18 } },
    tooltip: {},
    legend: { show: false },
    xAxis: { data: labels, axisLabel: { color: '#E7F9F7' } },
    yAxis: { axisLabel: { color: '#E7F9F7' } },
    series: [{
      name: datasetLabel || 'Data',
      type: 'bar',
      data: dataValues.map((v) => ({ value: v, itemStyle: { color: getColorByValue(v) } })),
    }],
  }
}