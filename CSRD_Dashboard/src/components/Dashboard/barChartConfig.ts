import type { ActiveElement, ChartEvent } from "chart.js"

const getColorByValue = (value: number): string => {
  if (value > 75) return '#68c14e'
  if (value >= 40) return '#f7b13c'
  if (value >= 1) return '#ba175e'
  return '#3b82f6'
}

export const createBarChartConfig = (
  labels: string[],
  dataValues: number[],
  title: string,
  datasetLabel?: string,
  onClickHandler?: (index: number) => void,
) => {
  return {
    data: {
      labels,
      datasets: [
        {
          label: datasetLabel || 'Data',
          backgroundColor: dataValues.map(getColorByValue),
          data: dataValues,
        },
      ],
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      scales: {
        x: {
          ticks: {
            color: '#ffffff',
          },
        },
        y: {
          ticks: {
            color: '#ffffff',
          },
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
          display: false,
        },
      },
      onClick: onClickHandler
        ? (evt: ChartEvent, elements: ActiveElement[]) => {
            if (!elements.length) return
            const index = elements[0]?.index ?? 0
            onClickHandler(index)
          }
        : undefined,
    },
  }
}

// Default configuration for the main dashboard
const mainLabels = ['Fase 1', 'Fase 2', 'Fase 3', 'Fase 4', 'Fase 5']
const mainDataValues = [40, 20, 80, 10, 50]

const mainConfig = createBarChartConfig(mainLabels, mainDataValues, 'Bar Chart Example', 'Data One')

export const data = mainConfig.data
export const options = mainConfig.options
