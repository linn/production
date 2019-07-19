import React from 'react';
import { createShallow } from '@material-ui/core/test-utils';
import AteFaultCodes from '../ate/AteFaultCodes';

describe('<AteFaultCodes />', () => {
    const shallow = createShallow({ dive: false });
    let wrapper;
    describe('when loaded', () => {
        beforeEach(() => {
            const props = {
                loading: false,
                items: [
                    {
                        faultCode: '1',
                        description: 'Description 1',
                        links: [{ rel: 'self', href: '/1' }]
                    },
                    {
                        faultCode: '2',
                        description: 'Description 2',
                        links: [{ rel: 'self', href: '/2' }]
                    },
                    {
                        faultCode: '3',
                        description: 'Description 3',
                        links: [{ rel: 'self', href: '/3' }]
                    }
                ]
            };
            wrapper = shallow(<AteFaultCodes {...props} />);
        });

        it('should render a table', () => {
            expect(wrapper.find('PaginatedTable')).toHaveLength(1);
        });
    });

    describe('when not loaded', () => {
        beforeEach(() => {
            const props = {
                loading: true,
                items: []
            };
            wrapper = shallow(<AteFaultCodes {...props} />);
        });

        it('should render the loading dialog', () => {
            expect(wrapper.find('Loading')).toHaveLength(1);
        });
    });
});
