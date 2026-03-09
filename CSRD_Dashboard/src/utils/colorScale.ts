export const getColorByValue = (value: number): string => {
  if (value > 75) return '#ABD006'
  if (value >= 40) return '#FCFF66'
  if (value >= 1) return '#FF967E'
  return '#D6F5F2'
}

export const getBorderColorByValue = (value: number): string => {
  if (value > 75) return 'border-[#ABD006]'
  if (value >= 40) return 'border-[#FCFF66]'
  if (value >= 1) return 'border-[#FF967E]'
  return 'border-[#D6F5F2]'
}

export const getTextColorByValue = (value: number): string => {
  if (value > 75) return 'text-[#ABD006]'
  if (value >= 40) return 'text-[#FCFF66]'
  if (value >= 1) return 'text-[#FF967E]'
  return 'text-[#D6F5F2]'
}