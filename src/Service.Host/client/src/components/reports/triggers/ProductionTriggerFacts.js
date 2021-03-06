import React from 'react';
import Grid from '@material-ui/core/Grid';
import Link from '@material-ui/core/Link';
import { Link as RouterLink } from 'react-router-dom';
import { Loading, Title, ErrorCard } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import NotesIcon from '@material-ui/icons/Notes';
import Page from '../../../containers/Page';
import FactList from './FactList';
import FactListItem from './FactListItem';
import FactListDetails from './FactListDetails';
import WorksOrderList from './WorksOrderList';
import SalesOrderList from './SalesOrderList';
import WhereUsedAssembliesList from './WhereUsedAssembliesList';
import priorityText from './priorityText';

function ProductionTriggerFacts({ reportData, loading, itemError }) {
    return (
        <Page>
            <Grid container spacing={3} justify="center">
                {loading ? (
                    <Grid item xs={12}>
                        <Title text="Loading Production Trigger Facts for a Part" />
                        <Loading />
                    </Grid>
                ) : (
                    ''
                )}
                {itemError ? (
                    <ErrorCard errorMessage={itemError.statusText} />
                ) : (
                    <Grid item xs={12}>
                        {reportData ? (
                            <>
                                <Title text="Production Trigger Facts for a Part" />
                                <Link
                                    component={RouterLink}
                                    to={`/production/reports/triggers?jobref=${reportData.jobref}&citCode=${reportData.citcode}`}
                                >
                                    From jobref {reportData.jobref} CIT {reportData.citName}
                                </Link>
                                <FactList>
                                    <FactListItem
                                        header={reportData.partNumber}
                                        secondary={reportData.description}
                                    />
                                    <FactListItem
                                        header="Qty Free"
                                        secondary="Good stock available to use"
                                        avatar={reportData.qtyFree}
                                    >
                                        <FactListDetails
                                            details={[
                                                {
                                                    header: 'Good Stock',
                                                    value: reportData.qtyYFlagged,
                                                    notes: 'Y Flagged'
                                                },
                                                {
                                                    header: 'Uninspected Stock',
                                                    value: reportData.qtyNFlagged,
                                                    notes: 'N Flagged'
                                                },
                                                {
                                                    header: 'Failed Stock',
                                                    value: reportData.qtyFFlagged,
                                                    notes: 'F Flagged'
                                                }
                                            ]}
                                        />
                                    </FactListItem>
                                    <FactListItem
                                        header="Qty Being Built"
                                        secondary="Outstanding works order qty"
                                        avatar={reportData.qtyBeingBuilt}
                                    >
                                        <WorksOrderList
                                            worksOrders={reportData.outstandingWorksOrders}
                                        />
                                    </FactListItem>
                                    <FactListItem
                                        header="Back-Order Requirement"
                                        secondary="Required for sales customers back orders"
                                        avatar={reportData.reqtForSalesOrdersBE}
                                    >
                                        <>
                                            <SalesOrderList
                                                salesOrders={reportData.productionBackOrders}
                                            />
                                        </>
                                    </FactListItem>
                                    <FactListItem
                                        header="Required for Internal Customers"
                                        secondary="Required to satisfy all your internal customers"
                                        avatar={reportData.reqtForInternalCustomersGBI}
                                    >
                                        <WhereUsedAssembliesList
                                            assemblies={reportData.whereUsedAssemblies}
                                        />
                                    </FactListItem>
                                    <FactListItem
                                        header="Trigger Level"
                                        secondary={`You should have at least this in stock after customers have been satisfied. ${reportData.triggerLevelText}`}
                                        avatar={reportData.effectiveTriggerLevel}
                                    >
                                        <FactListDetails
                                            details={[
                                                {
                                                    header: 'Variable Trigger Level',
                                                    value: reportData.variableTriggerLevel,
                                                    notes:
                                                        'Set by forecasting system for certain parts'
                                                },
                                                {
                                                    header: 'Override Trigger Level',
                                                    value: reportData.overrideTriggerLevel,
                                                    notes:
                                                        'Set manually in production trigger levels utility'
                                                }
                                            ]}
                                        />
                                    </FactListItem>
                                    <FactListItem
                                        header="Kanban Size"
                                        secondary="You must always build in multiples of this"
                                        avatar={reportData.kanbanSize}
                                    />
                                    {reportData.remainingBuild ? (
                                        <FactListItem
                                            header="Remaining Fixed Build"
                                            secondary="The fixed build for the next 7 days minus what has already been built"
                                            avatar={reportData.remainingBuild}
                                        />
                                    ) : (
                                        <FactListItem
                                            header="Build"
                                            secondary="Build this to satisfy internal and external customers, and trigger level"
                                            avatar={reportData.reqtForInternalAndTriggerLevelBT}
                                        >
                                            <FactListDetails
                                                details={[
                                                    {
                                                        header: 'Cit',
                                                        value: reportData.citName
                                                    },
                                                    {
                                                        header: 'PTL Jobref',
                                                        value: reportData.jobref
                                                    }
                                                ]}
                                            />
                                        </FactListItem>
                                    )}
                                    <FactListItem
                                        header="Priority"
                                        secondary={priorityText(reportData.priority)}
                                        avatar={reportData.priority}
                                    >
                                        <FactListDetails
                                            details={[
                                                {
                                                    header: '1',
                                                    value: `${priorityText('1')}`
                                                },
                                                {
                                                    header: '2',
                                                    value: `${priorityText('2')}`
                                                },
                                                {
                                                    header: '3',
                                                    value: `${priorityText('3')}`
                                                },
                                                {
                                                    header: '4',
                                                    value: `${priorityText('4')}`
                                                },
                                                {
                                                    header: '5',
                                                    value: `${priorityText('5')}`
                                                }
                                            ]}
                                        />
                                    </FactListItem>
                                    <FactListItem
                                        header="Story"
                                        secondary={
                                            reportData.story ? reportData.story : 'No story entered'
                                        }
                                        avatar={<NotesIcon />}
                                    />
                                </FactList>
                            </>
                        ) : (
                            ''
                        )}
                    </Grid>
                )}
            </Grid>
        </Page>
    );
}

ProductionTriggerFacts.propTypes = {
    reportData: PropTypes.shape({
        story: PropTypes.string,
        priority: PropTypes.string,
        jobref: PropTypes.string,
        citName: PropTypes.string,
        citcode: PropTypes.string,
        reqtForInternalAndTriggerLevelBT: PropTypes.number,
        remainingBuild: PropTypes.number,
        kanbanSize: PropTypes.number,
        variableTriggerLevel: PropTypes.number,
        effectiveTriggerLevel: PropTypes.number,
        reqtForInternalCustomersGBI: PropTypes.number,
        reqtForSalesOrdersBE: PropTypes.number,
        overrideTriggerLevel: PropTypes.string,
        whereUsedAssemblies: PropTypes.arrayOf(PropTypes.string),
        productionBackOrders: PropTypes.arrayOf(PropTypes.string),
        outstandingWorksOrders: PropTypes.arrayOf(PropTypes.string),
        triggerLevelText: PropTypes.string,
        qtyBeingBuilt: PropTypes.number,
        qtyFFlagged: PropTypes.number,
        qtyYFlagged: PropTypes.number,
        qtyNFlagged: PropTypes.number,
        partNumber: PropTypes.string,
        qtyFree: PropTypes.number,
        description: PropTypes.string
    }),
    loading: PropTypes.bool,
    config: PropTypes.shape({}),
    history: PropTypes.shape({}).isRequired,
    itemError: PropTypes.shape({ statusText: PropTypes.string })
};

ProductionTriggerFacts.defaultProps = {
    reportData: null,
    config: null,
    loading: false,
    itemError: null
};

export default ProductionTriggerFacts;
