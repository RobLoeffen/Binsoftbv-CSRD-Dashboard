export interface EcopartMaterial {
  name: string
  density: number
  emissionFactor: number
}

export interface EcopartDimensions {
  Length?: number
  Width?: number
  Height?: number
  Radius?: number
}

export interface EcopartShape {
  shapeType: string
  dimensions: EcopartDimensions
}

export interface Ecopart {
  id: string
  name: string
  material: EcopartMaterial
  shape: EcopartShape
}

export interface EcopartDetail extends Ecopart {
  mass: number
  carbonFootprint: number
}
