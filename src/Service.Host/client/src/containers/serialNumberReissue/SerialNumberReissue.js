import { connect } from 'react-redux';
import { fetchErrorSelectors } from '@linn-it/linn-form-components-library';
import SerialNumberReissue from '../../components/serialNumberReissue/SerialNumberReissue';
import serialNumberReissueActions from '../../actions/serialNumberReissueActions';
import serialNumberReissueSelectors from '../../selectors/serialNumberReissueSelectors';

const mapStateToProps = state => ({
    item: serialNumberReissueSelectors.getItem(state),
    editStatus: serialNumberReissueSelectors.getEditStatus(state),
    loading: serialNumberReissueSelectors.getLoading(state),
    snackbarVisible: serialNumberReissueSelectors.getSnackbarVisible(state),
    errorMessage: fetchErrorSelectors(state)
});

const mapDispatchToProps = {
    addItem: serialNumberReissueActions.add
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(SerialNumberReissue);
