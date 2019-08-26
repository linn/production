import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../../test-utils';
import ProductionMeasures from '../../reports/measures/ProductionMeasures';

describe('<ProductionMeasures />', () => {
    afterEach(cleanup);

    const defaultProps = {
        loading: false,
        citsData: null,
        infoData: null,
        config: {
            approot: 'app.linn.co.uk'
        }
    };

    describe('When Loading', () => {
        it('should display spinner', () => {
            const { getAllByRole } = render(<ProductionMeasures {...defaultProps} loading />);
            expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
        });
    });

    describe('When Not Loading', () => {
        it('should not display spinner', () => {
            const { queryByRole } = render(<ProductionMeasures {...defaultProps} />);
            expect(queryByRole('progressbar')).toBeNull();
        });
    });

    describe('When no citsData', () => {
        it('should not display table', () => {
            const { queryByRole } = render(<ProductionMeasures {...defaultProps} />);
            expect(queryByRole('table')).toBeNull();
        });
    });

    describe('When citsData', () => {
        it('should display table', () => {
            const cits = [{ citName: 'A' }];
            const info = { lastPtlJobref: 'AAAAAA' };
            const { getAllByRole } = render(
                <ProductionMeasures {...defaultProps} citsData={cits} infoData={info}/>
            );
            expect(getAllByRole('table').length).toBeGreaterThan(0);
        });
    });
});
