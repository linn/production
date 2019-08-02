import { connect } from 'react-redux';
import { fetchErrorSelectors } from '@linn-it/linn-form-components-library';
import SerialNumberReissue from '../../components/serialNumberReissue/SerialNumberReissue';
import serialNumberReissueActions from '../../actions/serialNumberReissueActions';
import serialNumberReissueSelectors from '../../selectors/serialNumberReissueSelectors';
import serialNumberActions from '../../actions/serialNumberActions';
import serialNumberSelectors from '../../selectors/serialNumberSelectors';

const mapStateToProps = state => ({
    editStatus: serialNumberReissueSelectors.getEditStatus(state),
    errorMessage: fetchErrorSelectors(state),
    item: serialNumberReissueSelectors.getItem(state),
    loading: serialNumberReissueSelectors.getLoading(state),
    serialNumbers: serialNumberSelectors.getItem(state),
    serialNumbersLoading: serialNumberSelectors.getLoading(state),
    snackbarVisible: serialNumberReissueSelectors.getSnackbarVisible(state)
});

const mapDispatchToProps = {
    addItem: serialNumberReissueActions.add,
    fetchSerialNumbers: serialNumberActions.fetchByQueryString,
    setSnackbarVisible: serialNumberReissueActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(SerialNumberReissue);
