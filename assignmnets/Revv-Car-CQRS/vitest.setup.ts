// vitest.setup.ts
import { config } from '@vue/test-utils'

config.global.stubs = {
  'font-awesome-icon': {
    template: '<span class="mock-icon" />'
  }
}
