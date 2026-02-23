import router from '@/router'
import type { ActiveElement, ChartEvent } from 'chart.js'

const getColorByValue = (value: number): string => {
  if (value > 75) return '#68c14e'
  if (value >= 40) return '#f7b13c'
  if (value >= 1) return '#ba175e'
  return '#3b82f6'
}

export const createPieChartConfig = (
  labels: string[],
  dataValues: number[],
  title: string,
  onClickHandler?: (index: number) => void,
) => {
  return {
    data: {
      labels,
      datasets: [
        {
          backgroundColor: dataValues.map(getColorByValue),
          data: dataValues,
        },
      ],
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      layout: {
        padding: {
          top: 10,
          bottom: 10,
          left: 10,
          right: 10,
        },
      },
      plugins: {
        title: {
          display: true,
          text: title,
          color: '#ffffff',
          font: {
            size: 18,
          },
        },
        legend: {
          display: true,
          labels: {
            color: '#ffffff',
            padding: 5,
            boxWidth: 20,
            boxHeight: 20,
          },
        },
      },
      onClick: (evt: ChartEvent, elements: ActiveElement[]) => {
        if (!elements.length) return
        const index = elements[0]?.index ?? 0
        if (onClickHandler) {
          onClickHandler(index)
        }
      },
    },
  }
}

const mainLabels = ['Fase 1', 'Fase 2', 'Fase 3', 'Fase 4', 'Fase 5']
const mainDataValues = [40, 20, 80, 10, 50]

const mainConfig = createPieChartConfig(mainLabels, mainDataValues, 'CSRD fases', (index) => {
  router.push(`/fase/${index + 1}`)
})

export const data = mainConfig.data
export const options = mainConfig.options
