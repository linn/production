import { connect } from 'react-redux';
import {
    getItemErrors,
    getItemErrorDetailMessage,
    initialiseOnMount,
    getPreviousPaths
} from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import WorksOrder from '../../components/worksOrders/WorksOrder';
import worksOrderSelectors from '../../selectors/worksOrderSelectors';
import worksOrderActions from '../../actions/worksOrderActions';
import employeesSelectors from '../../selectors/employeesSelectors';
import employeesActions from '../../actions/employeesActions';
import worksOrderDetailsActions from '../../actions/worksOrderDetailsActions';
import worksOrderDetailsSelectors from '../../selectors/worksOrderDetailsSelectors';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import printWorksOrderLabelActions from '../../actions/printWorksOrderLabelsActions';
import printWorksOrderAioLabelActions from '../../actions/printWorksOrderAioLabelsActions';
import printWorksOrderLabelsSelectors from '../../selectors/printWorksOrderLabelsSelectors';
import printWorksOrderAioLabelsSelectors from '../../selectors/printWorksOrderAioLabelsSelectors';
import * as itemTypes from '../../itemTypes';
import * as processTypes from '../../processTypes';

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options || {};
};

const mapStateToProps = (state, ownProps) => ({
    itemErrors: getItemErrors(state),
    worksOrderError: getItemErrorDetailMessage(state, itemTypes.worksOrder.item),
    worksOrderDetailsError: getItemErrorDetailMessage(state, itemTypes.worksOrderDetails.item),
    editStatus: 'create',
    loading: worksOrderSelectors.getLoading(state),
    options: getOptions(ownProps),
    snackbarVisible: worksOrderSelectors.getSnackbarVisible(state),
    employees: employeesSelectors.getItems(state),
    worksOrderDetails: worksOrderDetailsSelectors.getItem(state),
    partsSearchLoading: partsSelectors.getSearchLoading(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state, 100)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber })),
    printWorksOrderLabelsErrorDetail: getItemErrorDetailMessage(
        state,
        processTypes.printWorksOrderLabels.item
    ),
    printWorksOrderLabelsMessageVisible: printWorksOrderLabelsSelectors.getMessageVisible(state),
    printWorksOrderLabelsMessageText: printWorksOrderLabelsSelectors.getMessageText(state),
    printWorksOrderAioLabelsErrorDetail: getItemErrorDetailMessage(
        state,
        processTypes.printWorksOrderAioLabels.item
    ),
    printWorksOrderAioLabelsMessageVisible: printWorksOrderAioLabelsSelectors.getMessageVisible(
        state
    ),
    printWorksOrderAioLabelsMessageText: printWorksOrderAioLabelsSelectors.getMessageText(state),
    previousPaths: getPreviousPaths(state)
});

const initialise = () => dispatch => {
    dispatch(employeesActions.fetch());
    dispatch(worksOrderDetailsActions.create());
    dispatch(worksOrderActions.create());
    dispatch(partsActions.clearSearch());
};

const mapDispatchToProps = {
    initialise,
    setSnackbarVisible: worksOrderActions.setSnackbarVisible,
    fetchWorksOrderDetails: worksOrderDetailsActions.fetch,
    addItem: worksOrderActions.add,
    updateItem: worksOrderActions.update,
    setEditStatus: worksOrderActions.setEditStatus,
    fetchWorksOrder: worksOrderActions.fetch,
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    printWorksOrderLabels: printWorksOrderLabelActions.requestProcessStart,
    clearPrintWorksOrderLabelsErrors: printWorksOrderLabelActions.clearErrorsForItem,
    setPrintWorksOrderLabelsMessageVisible: printWorksOrderLabelActions.setMessageVisible,
    printWorksOrderAioLabels: printWorksOrderAioLabelActions.requestProcessStart,
    clearPrintWorksOrderAioLabelsErrors: printWorksOrderAioLabelActions.clearErrorsForItem,
    setPrintWorksOrderAioLabelsMessageVisible: printWorksOrderAioLabelActions.setMessageVisible,
    clearErrors: worksOrderActions.clearErrorsForItem
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(WorksOrder));
