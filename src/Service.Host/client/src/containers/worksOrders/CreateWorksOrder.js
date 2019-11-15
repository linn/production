import { connect } from 'react-redux';
import {
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import WorksOrder from '../../components/worksOrders/WorksOrder';
import worksOrderSelectors from '../../selectors/worksOrderSelectors';
import worksOrderActions from '../../actions/worksOrderActions';
import employeesSelectors from '../../selectors/employeesSelectors';
import employeesActions from '../../actions/employeesActions';
import worksOrderDetailsActions from '../../actions/worksOrderDetailsActions';
import worksOrderDetailsSelectors from '../../selectors/worksOrderDetailsSelectors';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    worksOrderError: getItemErrorDetailMessage(state, itemTypes.worksOrder.item),
    worksOrderDetailsError: getItemErrorDetailMessage(state, itemTypes.worksOrderDetails.item),
    editStatus: 'create',
    loading: worksOrderSelectors.getLoading(state),
    snackbarVisible: worksOrderSelectors.getSnackbarVisible(state),
    employees: employeesSelectors.getItems(state),
    worksOrderDetails: worksOrderDetailsSelectors.getItem(state),
    partsSearchLoading: partsSelectors.getSearchLoading(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber }))
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
    clearPartsSearch: partsActions.clearSearch
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(WorksOrder));
