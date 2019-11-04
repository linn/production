import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import worksOrderLabelActions from '../../actions/worksOrderLabelActions';
import WorksOrderLabel from '../../components/worksOrders/WorksOrderLabel';
import worksOrderLabelSelectors from '../../selectors/worksOrderLabelSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    itemError: getItemError(state, itemTypes.worksOrderLabel.item),
    loading: worksOrderLabelSelectors.getLoading(state),
    snackbarVisible: worksOrderLabelSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(worksOrderLabelActions.setEditStatus('create'));
    dispatch(worksOrderLabelActions.clearErrorsForItem());
};

const mapDispatchToProps = {
    initialise,
    addItem: worksOrderLabelActions.add,
    setEditStatus: worksOrderLabelActions.setEditStatus,
    setSnackbarVisible: worksOrderLabelActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(WorksOrderLabel));
