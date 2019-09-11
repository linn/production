import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../../test-utils';
import TriggersList from '../../reports/triggers/TriggersList';

describe('<TriggersList />', () => {
    afterEach(cleanup);

    const triggers = [
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
    ];

    describe('When showing full report', () => {
        it('should display three parts', () => {
            const { getByText } = render(
                <TriggersList triggers={triggers} jobref="AAAAAA" reportFormat="FULL" getall />
            );
            const firstPart = getByText('SERIES K');
            const secondPart = getByText('LP12MS/2');
            const thirdPart = getByText('MAJIK THING');
            expect(firstPart).toBeInTheDocument();
            expect(secondPart).toBeInTheDocument();
            expect(thirdPart).toBeInTheDocument();
        });
    });

    describe('When showing brief report', () => {
        it('should display two parts', () => {
            const { getByText, queryByText } = render(
                <TriggersList triggers={triggers} jobref="AAAAAA" reportFormat="BRIEF" getall />
            );
            const firstPart = getByText('SERIES K');
            const secondPart = getByText('LP12MS/2');
            expect(firstPart).toBeInTheDocument();
            expect(secondPart).toBeInTheDocument();
            expect(queryByText('MAJIK THING')).toBeNull();
        });
    });
});
