import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../../test-utils';
import ProductionTriggers from '../../reports/triggers/ProductionTriggers';

describe('<ProductionTriggers />', () => {
    afterEach(cleanup);

    const defaultProps = {
        reportData: null,
        loading: false,
        cits: [{ code: 'A', name: 'A CIT' }],
        options: null
    };

    describe('When Loading', () => {
        it('should display spinner', () => {
            const { getAllByRole } = render(<ProductionTriggers {...defaultProps} loading />);
            expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
        });
    });

    describe('When Rendering Reportdata', () => {
        const defaultReportData = {
            citCode: 'A',
            ptlJobref: 'AAAAAA',
            ptlRunDateTime: new Date(),
            triggers: [
                {
                    partNumber: 'SERIES K',
                    description: 'SERIES K CONTROLLERNATOR',
                    reqtForInternalAndTriggerLevelBT: 4,
                    priority: 1,
                    canBuild: 4,
                    kanbanSize: 1,
                    qtyBeingBuilt: 0,
                    story: 'Neverending story',
                    reportFormat: 'BRIEF'
                },
                {
                    partNumber: 'LP12MS/2',
                    description: 'LP12 IN MAPLE SAUCE',
                    reqtForInternalAndTriggerLevelBT: 1,
                    priority: 1,
                    canBuild: 1,
                    kanbanSize: 1,
                    qtyBeingBuilt: 0,
                    reportFormat: 'BRIEF'
                },
                {
                    partNumber: 'MAJIK THING',
                    description: 'ITS A KIND OF MAJIK',
                    reqtForInternalAndTriggerLevelBT: 10,
                    priority: 5,
                    canBuild: 0,
                    kanbanSize: 1,
                    qtyBeingBuilt: 0,
                    reportFormat: 'FULL'
                }
            ]
        }

        it('should not display spinner', () => {
            const { queryByRole } = render(
                <ProductionTriggers {...defaultProps} reportData={defaultReportData} />
            );
            expect(queryByRole('progressbar')).toBeNull();
        });

        describe('When showing brief report', () => {
            it('should display two parts', () => {
                const { getByText, queryByText } = render(
                    <ProductionTriggers {...defaultProps} reportData={defaultReportData} />
                );
                const firstPart = getByText('SERIES K');
                const secondPart = getByText('LP12MS/2');
                expect(firstPart).toBeInTheDocument();
                expect(secondPart).toBeInTheDocument();
                expect(queryByText('MAJIK THING')).toBeNull();
            });
        });
    });
});