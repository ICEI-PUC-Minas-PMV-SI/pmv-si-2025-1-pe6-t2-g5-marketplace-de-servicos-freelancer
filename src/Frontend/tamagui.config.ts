import { defaultConfig } from '@tamagui/config/v4'
import { createTamagui } from 'tamagui'

const config = createTamagui(defaultConfig)

type CustomConfig = typeof config

// ensure types work
declare module 'tamagui' {
  interface TamaguiCustomConfig extends CustomConfig {}
}

export default config;