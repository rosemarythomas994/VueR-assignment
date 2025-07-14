import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import { mount } from '@vue/test-utils'
import Cards from '../Cards.vue'  // ✅ Ensure path is correct

// ✅ Mock font-awesome-icon globally
vi.mock('@fortawesome/vue-fontawesome', () => ({
  FontAwesomeIcon: {
    template: '<span class="mock-icon" />',
  },
}))

let wrapper: ReturnType<typeof mount>

beforeEach(() => {
  wrapper = mount(Cards)
})

afterEach(() => {
  wrapper.unmount()
})

describe('Cards.vue', () => {
  it('renders the correct number of cards', () => {
    const cards = wrapper.findAll('.green-card')
    expect(cards).toHaveLength(3)
  })

  it('renders correct content for each card', () => {
    const expected = [
      {
        title: 'AI-Driven',
        description: 'We streamline and simplify your processes with cutting-edge AI.',
      },
      {
        title: 'Data-Driven',
        description: 'We ensure transparent and informed decision-making using data.',
      },
      {
        title: 'Customer Focused',
        description: 'Guiding your remarketing needs with powerful insights.',
      },
    ]

    expected.forEach(({ title, description }, index) => {
      const card = wrapper.findAll('.green-card')[index]
      expect(card.text()).toContain(title)
      expect(card.text()).toContain(description)
    })
  })

  it('renders mocked icons', () => {
    const icons = wrapper.findAll('.mock-icon')
    expect(icons).toHaveLength(3)
  })
})
