import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../../test-utils';
import ProductionTriggerFacts from '../../reports/triggers/ProductionTriggerFacts';

describe('<ProductionTriggerFacts />', () => {
    afterEach(cleanup);

    const defaultProps = {
        reportData: null,
        loading: false
    };

    describe('When Loading', () => {
        it('should display spinner', () => {
            const { getAllByRole } = render(<ProductionTriggerFacts {...defaultProps} loading />);
            expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
        });
    });

    describe('When Rendering Reportdata', () => {
        const defaultReportData = {
            jobref: 'AAAAAA',
            partNumber: 'AKUB/B',
            description: 'Exakt Akubarik Speaker',
            citCode: 'A',
            citName: 'A CIT',
            triggerLevel: 0,
            nettSalesOrders: 0,
            reqtForInternalAndTriggerLevelBT: 1,
            qtyFree: 0,
            qtyBeingBuilt: 1,
            priority: '1',
            reqtForSalesOrdersBE: 1,
            reqtForInternalCustomersGBI: 1,
            effectiveTriggerLevel: 1,
            kanbanSize: 10,
            outstandingWorksOrders: [],
            remainingBuild: 1,
            productionBackOrders: [],
            whereUsedAssemblies: []
        };

        it('should not display spinner', () => {
            const { queryByRole } = render(
                <ProductionTriggerFacts {...defaultProps} reportData={defaultReportData} />
            );
            expect(queryByRole('progressbar')).toBeNull();
        });

        describe('When facts', () => {
            it('should display facts', () => {
                const { getByText } = render(
                    <ProductionTriggerFacts {...defaultProps} reportData={defaultReportData} />
                );
                const akubPart = getByText('AKUB/B');
                expect(akubPart).toBeInTheDocument();
            });
        });
    });
});
