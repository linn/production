import React, { Fragment } from 'react';
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

function ProductionTriggerFacts({ reportData, loading, options, history, itemError }) {
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
                            <Fragment>
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
                                        avatar={<span> reportData.qtyFree </span>}
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
                                        avatar={<span> reportData.qtyBeingBuilt </span>}
                                    >
                                        <WorksOrderList
                                            worksOrders={reportData.outstandingWorksOrders}
                                        />
                                    </FactListItem>
                                    <FactListItem
                                        header="Back-Order Requirement"
                                        secondary="Required for sales customers back orders"
                                        avatar={<span> reportData.reqtForSalesOrdersBE </span>}
                                    >
                                        <span>
                                            <SalesOrderList
                                                salesOrders={reportData.productionBackOrders}
                                            />
                                        </span>
                                    </FactListItem>
                                    <FactListItem
                                        header="Required for Internal Customers"
                                        secondary="Required to satisfy all your internal customers"
                                        avatar={<span>reportData.reqtForInternalCustomersGBI</span>}
                                    >
                                        <WhereUsedAssembliesList
                                            assemblies={reportData.whereUsedAssemblies}
                                        />
                                    </FactListItem>
                                    <FactListItem
                                        header="Trigger Level"
                                        secondary={`You should have at least this in stock after customers have been satisfied. ${reportData.triggerLevelText}`}
                                        avatar={<span> reportData.effectiveTriggerLevel </span>}
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
                                        avatar={<span> reportData.kanbanSize </span>}
                                    />
                                    {reportData.remainingBuild ? (
                                        <FactListItem
                                            header="Remaining Fixed Build"
                                            secondary="The fixed build for the next 7 days minus what has already been built"
                                            avatar={<span> reportData.remainingBuild </span>}
                                        />
                                    ) : (
                                        <FactListItem
                                            header="Build"
                                            secondary="Build this to satisfy internal and external customers, and trigger level"
                                            avatar={
                                                <span>
                                                    reportData.reqtForInternalAndTriggerLevelBT
                                                </span>
                                            }
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
                                        avatar={<span> reportData.priority </span>}
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
                            </Fragment>
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
    reportData: PropTypes.shape({}),
    loading: PropTypes.bool,
    config: PropTypes.shape({})
};

ProductionTriggerFacts.defaultProps = {
    reportData: null,
    config: null,
    loading: false
};

export default ProductionTriggerFacts;
