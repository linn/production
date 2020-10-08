import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../test-utils';
import BuildPlans from '../buildPlans/BuildPlans';

afterEach(cleanup);

const defaultProps = {
    searchParts: jest.fn(),
    clearPartsSearch: jest.fn(),
    updateBuildPlan: jest.fn(),
    history: { push: jest.fn() },
    updateBuildPlanDetail: jest.fn(),
    saveBuildPlanDetail: jest.fn(),
    fetchBuildPlanDetails: jest.fn(),
    fetchBuildPlans: jest.fn(),
    fetchBuildPlanRules: jest.fn(),
    setBuildPlanSnackbarVisible: jest.fn(),
    setBuildPlanDetailSnackbarVisible: jest.fn(),
    buildPlansLoading: false,
    buildPlanDetailsLoading: false,
    buildPlanRulesLoading: false,
    buildPlanDetailLoading: false,
    buildPlans: [
        {
            buildPlanName: 'name1',
            description: 'desc1',
            dateCreated: '2020-02-05T11:39:26.0000000',
            dateInvalid: '2022-02-05T11:39:26.0000000'
        },
        {
            buildPlanName: 'name2',
            description: 'desc2',
            dateCreated: '2020-02-05T11:39:26.0000000',
            dateInvalid: '2022-02-05T11:39:26.0000000'
        }
    ],
    buildPlanDetails: [
        {
            buildPlanName: 'name1',
            partNumber: 'part1',
            fromDate: '2009-02-05T11:39:26.0000000',
            toDate: '2010-02-05T11:39:26.0000000',
            ruleCode: 'rule1',
            quantity: 111,
            partDescription: 'partDesc1'
        },
        {
            buildPlanName: 'name1',
            partNumber: 'part2',
            fromDate: '2011-02-05T11:39:26.0000000',
            toDate: '2012-02-05T11:39:26.0000000',
            ruleCode: 'rule2',
            quantity: 222,
            partDescription: 'partDesc2'
        },
        {
            buildPlanName: 'name2',
            partNumber: 'part3',
            fromDate: '2013-02-05T11:39:26.0000000',
            toDate: '2014-02-05T11:39:26.0000000',
            ruleCode: 'rule3',
            quantity: 333,
            partDescription: 'partDesc3'
        },
        {
            buildPlanName: 'name2',
            partNumber: 'part4',
            fromDate: '2015-02-05T11:39:26.0000000',
            toDate: '2016-02-05T11:39:26.0000000',
            ruleCode: 'rule4',
            quantity: 444,
            partDescription: 'partDesc4'
        }
    ],
    buildPlanRules: [
        {
            ruleCode: 'code1',
            description: 'codeDesc1'
        },
        {
            ruleCode: 'code2',
            description: 'codeDesc2'
        },
        {
            ruleCode: 'code3',
            description: 'codeDesc3'
        },
        {
            ruleCode: 'code4',
            description: 'codeDesc4'
        }
    ]
};

describe('<BuildPlans />', () => {
    describe('loading states', () => {
        describe('when build plans loading', () => {
            it('should display spinner', () => {
                const { getAllByRole } = render(<BuildPlans {...defaultProps} buildPlansLoading />);
                expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
            });

            it('should not display form fields', () => {
                const { queryByRole } = render(<BuildPlans {...defaultProps} buildPlansLoading />);
                expect(queryByRole('input')).not.toBeInTheDocument();
            });
        });

        describe('when build plan details loading', () => {
            it('should display spinner', () => {
                const { getAllByRole } = render(
                    <BuildPlans {...defaultProps} buildPlanDetailsLoading />
                );
                expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
            });

            it('should not display form fields', () => {
                const { queryByRole } = render(
                    <BuildPlans {...defaultProps} buildPlanDetailsLoading />
                );
                expect(queryByRole('input')).not.toBeInTheDocument();
            });
        });

        describe('when build plan rules loading', () => {
            it('should display spinner', () => {
                const { getAllByRole } = render(
                    <BuildPlans {...defaultProps} buildPlanRulesLoading />
                );
                expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
            });

            it('should not display form fields', () => {
                const { queryByRole } = render(
                    <BuildPlans {...defaultProps} buildPlanRulesLoading />
                );
                expect(queryByRole('input')).not.toBeInTheDocument();
            });
        });

        describe('when build plan detail loading', () => {
            it('should display spinner', () => {
                const { getAllByRole } = render(
                    <BuildPlans {...defaultProps} buildPlanDetailLoading />
                );
                expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
            });

            it('should not display form fields', () => {
                const { queryByRole } = render(
                    <BuildPlans {...defaultProps} buildPlanDetailLoading />
                );
                expect(queryByRole('input')).not.toBeInTheDocument();
            });
        });
    });

    describe('when build plan snackbar visible', () => {
        it('should render snackbar', () => {
            const { getByText } = render(<BuildPlans {...defaultProps} buildPlanSnackbarVisible />);
            const item = getByText('Save Successful');
            expect(item).toBeInTheDocument();
        });
    });

    describe('when build plan detail snackbar visible', () => {
        it('should render snackbar', () => {
            const { getByText } = render(
                <BuildPlans {...defaultProps} buildPlanDetailSnackbarVisible />
            );
            const item = getByText('Save Successful');
            expect(item).toBeInTheDocument();
        });
    });
});
